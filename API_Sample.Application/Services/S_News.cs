using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Data.EF;
using API_Sample.Data.Entities;
using API_Sample.Models.Common;
using API_Sample.Models.Request;
using API_Sample.Models.Response;
using API_Sample.Utilities.Constants;
using API_Sample.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace API_Sample.Application.Services
{
    public interface IS_News
    {
        Task<ResponseData<MRes_News>> Create(MReq_News request);

        Task<ResponseData<MRes_News>> Update(MReq_News request);

        Task<ResponseData<MRes_News>> UpdateStatus(int post_id, bool status, string updatedBy);

        Task<ResponseData<int>> Delete(int id);

        Task<ResponseData<MRes_News>> GetById(int id);

        Task<ResponseData<List<MRes_News>>> GetListByStatus(bool status);

        public Task<ResponseData<string>> AddAvatarNews(IFormFile file, int news_id);
    }
    public class S_News : IS_News
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        private readonly DriveService _driveService;

        public S_News(MainDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;

            // Đường dẫn tệp tin credentials.json
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GoogleDriveConfig", "webcrd-credentials.json");

            // Đường dẫn tệp cache để lưu trữ thông tin xác thực
            var cacheFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GoogleDriveConfig", "google-credentials-cache.json");

            GoogleCredential credential;

            if (File.Exists(cacheFilePath))
            {
                // Đọc thông tin credential đã lưu trong cache
                using (var stream = new FileStream(cacheFilePath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                                 .CreateScoped(DriveService.ScopeConstants.DriveFile);
                }
            }
            else
            {
                // Nếu không có file cache, tạo mới từ credentials.json
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Không tìm thấy file cấu hình Google Drive!", path);
                }

                credential = GoogleCredential.FromFile(path)
                                 .CreateScoped(DriveService.ScopeConstants.DriveFile);

                // Lưu thông tin credential vào file cache để sử dụng lần sau
                using (var stream = new FileStream(cacheFilePath, FileMode.Create, FileAccess.Write))
                {
                    var jsonCredential = credential.ToString(); // Không thể sử dụng .ToJson, thay vào đó dùng .ToString
                    var writer = new StreamWriter(stream);
                    writer.Write(jsonCredential);
                }
            }

            // Khởi tạo Google Drive Service
            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "webcrd"
            });
        }
        public async Task<ResponseData<string>> AddAvatarNews(IFormFile file, int news_id)
        {
            var res = new ResponseData<string>();
            try
            {
                if (file == null || file.Length == 0)
                {
                    res.error.message = "File không hợp lệ!";
                    return res;
                }
                var news_image = new NewsImage();

                var avatar_url = await UploadFileAsync(file, "12YdwkzCOVuUKqCPbEI79FFUpf_f2v_2F");

                // kiểm tra avatar_url ngay đây

                if (avatar_url == null)
                {
                    res.error.code = 400;
                    res.error.message = "Thêm ảnh cho cho tin tức thất bại!";
                    return res;
                }

                news_image.ImageUrl = avatar_url;
                news_image.NewsId = news_id;
                news_image.IsAvatar = true;
                news_image.CreateAt = DateTime.Now;

                _context.NewsImages.Update(news_image);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                res.data = avatar_url;
                res.result = 1;
                res.error.message = "Thêm ảnh cho tin tức thành công!";
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_News>> Create(MReq_News request)
        {
            var res = new ResponseData<MRes_News>();
            try
            {
                var data = new News();
                data.NewsName = request.NewsName;
                data.NameSlug =request.NameSlug;
                data.ShortDescription = request.ShortDescription;
                data.Content = request.Content;
                data.NewsCategoryId = request.NewsCategoryId;
                data.Status = request.IsActive;
                data.CreateAt = DateTime.Now;
                data.CreateBy = request.CreateBy;
                _context.News.Add(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_CREATE;
                    return res;
                }
                var getById = await GetById(data.NewsId);
                res.data = getById.data;
                res.result = 1;
                res.error.code = 201;
                res.error.message = MessageErrorConstants.CREATE_SUCCESS;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        public async Task<ResponseData<int>> Delete(int id)
        {
            var res = new ResponseData<int>();
            try
            {
                var data = await _context.News.FindAsync(id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                _context.News.Remove(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_DELETE;
                    return res;
                }
                res.data = save;
                res.result = 1;
                res.error.message = MessageErrorConstants.DELETE_SUCCESS;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_News>> GetById(int news_id)
        {
            var res = new ResponseData<MRes_News>();
            try
            {
                var data = await _context.News.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.NewsId == news_id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                var mapData = _mapper.Map<MRes_News>(data);
                res.data = mapData;
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        public async Task<ResponseData<List<MRes_News>>> GetListByStatus(bool status)
        {
            var res = new ResponseData<List<MRes_News>>();
            try
            {
                var data = await _context.News.AsNoTracking().Where(x => x.Status == status).OrderBy(x => x.NewsId).ToListAsync();
                res.data = _mapper.Map<List<MRes_News>>(data);
                res.result = 1;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_News>> Update(MReq_News request)
        {
            var res = new ResponseData<MRes_News>();
            try
            {

                var data = await _context.News.FindAsync(request.NewsId);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                data.NewsName = request.NewsName;
                data.NameSlug = request.NameSlug;
                data.ShortDescription = request.ShortDescription;
                data.Content = request.Content;
                data.NewsCategoryId = request.NewsCategoryId;
                data.Status = request.IsActive;
                data.UpdateAt = DateTime.Now;
                data.UpdateBy = request.UpdateBy;
                _context.Update(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var getById = await GetById(data.NewsId);
                res.data = getById.data;
                res.result = 1;
                res.error.message = MessageErrorConstants.UPDATE_SUCCESS;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        public async Task<ResponseData<MRes_News>> UpdateStatus(int news_id, bool status, string updatedBy)
        {
            var res = new ResponseData<MRes_News>();
            try
            {
                var data = await _context.News.FindAsync(news_id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                data.Status = status;
                data.UpdateAt = DateTime.Now;
                data.UpdateBy = updatedBy;
                _context.Update(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var getById = await GetById(data.NewsId);
                res.data = getById.data;
                res.result = 1;
                res.error.message = MessageErrorConstants.UPDATE_SUCCESS;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }

        // google drive
        public async Task<string> UploadFileAsync(IFormFile file, string folderId = "12YdwkzCOVuUKqCPbEI79FFUpf_f2v_2F")
        {
            try
            {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = file.FileName,
                    //Parents = new List<string> { folderId } // Gán file vào thư mục cụ thể
                };

                using var stream = file.OpenReadStream();
                var request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
                request.Fields = "id";
                var progress = await request.UploadAsync();

                if (progress.Status != Google.Apis.Upload.UploadStatus.Completed)
                {
                    Console.WriteLine($"Upload thất bại: {progress.Exception?.Message}");
                    return null;
                }

                // Cấp quyền truy cập công khai
                var permission = new Google.Apis.Drive.v3.Data.Permission
                {
                    Type = "anyone",
                    Role = "reader"
                };
                await _driveService.Permissions.Create(permission, request.ResponseBody.Id).ExecuteAsync();

                string fileUrl = $"https://drive.google.com/file/d/{request.ResponseBody.Id}";
                Console.WriteLine($"File uploaded: {fileUrl}");

                return fileUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi upload file: {ex.Message}");
                return null;
            }
        }
    }
}
