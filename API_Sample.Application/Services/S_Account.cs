using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Data.EF;
using API_Sample.Models.Common;
using API_Sample.Models.Request;
using API_Sample.Models.Response;
using AutoMapper;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using API_Sample.Data.Entities;    // phải có dòng này
using API_Sample.Utilities.Constants;
using Microsoft.Extensions.Configuration;  // khai báo thư viện

// khai báo để jwwt
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

// Khai báo google.apis.drive.v3
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;




namespace API_Sample.Application.Services
{
    public interface IS_Account
    {
        Task<ResponseData<MRes_Account>> Create(MReq_Account request);
        Task<ResponseData<MRes_Account>> Update(MReq_Account request);
        Task<ResponseData<int>> Delete(string user_name);

        Task<ResponseData<MRes_Account>> GetByUserName(string user_name);
        public Task<ResponseData<MRes_Account>> Login(MReq_Account request);

        public Task<ResponseData<string>> ChangeAvatar(IFormFile file, string user_name);
    }
    public class S_Account : IS_Account
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _secretKey;

        private readonly DriveService _driveService;

        public S_Account(MainDbContext context, IMapper mapper, IConfiguration configuration) 
        { 
            _context = context;
            _mapper = mapper;
            _secretKey = configuration["AppSettings:SecretKey"];

            // setip google drive 

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GoogleDriveConfig", "webcrd-credentials.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Không tìm thấy file cấu hình Google Drive!", path);
            }

            var credential = GoogleCredential.FromFile(path) 
                .CreateScoped(DriveService.ScopeConstants.DriveFile);

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "webcrd"
            });
        }
        public async Task<ResponseData<MRes_Account>> Create(MReq_Account request)
        {
            var res = new ResponseData<MRes_Account>(); // biến trả về client
            try
            {
                var isExistsUsername = await _context.Accounts.FirstOrDefaultAsync(x => x.Username == request.Username) != null;

                // xử lí trùng lặp tên đăng nhập
                if (isExistsUsername)
                {
                    res.error.message = "Tên đăng nhập đã tồn tại!";
                    return res;
                }

                var data = new Account();
                data.Username = request.Username.ToUpper().Trim();
                data.PassWord = request.PassWord;
                data.AvatarUrl = request.AvatarUrl;
                data.EmployeeId = request.EmployeeId;
                data.RoleId = request.RoleId;
                _context.Accounts.Add(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_CREATE;
                    return res;
                }
                var getById = await GetByUserName(data.Username);
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

        public async Task<ResponseData<int>> Delete(string user_name)
        {
            var res = new ResponseData<int>();
            try
            {
                var data = await _context.Accounts.FindAsync(user_name);
                if (data == null)
                {
                    res.error.message = "Không tìm thấy User Name!";
                    return res;
                }

                _context.Accounts.Remove(data);
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

        public async Task<ResponseData<MRes_Account>> GetByUserName(string user_name)
        {
            var res = new ResponseData<MRes_Account>();
            try
            {
                var data = await _context.Accounts.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Username == user_name);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                var mapData = _mapper.Map<MRes_Account>(data);
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

        public async Task<ResponseData<MRes_Account>> Update(MReq_Account request)
        {
            var res = new ResponseData<MRes_Account>();
            try
            {
                request.Username = request.Username.ToUpper().Trim();
                var isExistsUsername = await _context.Accounts.FirstOrDefaultAsync(x => x.Username == request.Username) != null;
                if (!isExistsUsername)
                {
                    res.error.message = "Không tìm thấy tên người dùng!";
                    return res;
                }

                var data = await _context.Accounts.FindAsync(request.Username);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                data.AvatarUrl = request.AvatarUrl;
                data.EmployeeId = request.EmployeeId;
                data.RoleId = request.RoleId;
                _context.Update(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var getById = await GetByUserName(data.Username);
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

        public async Task<ResponseData<MRes_Account>> Login(MReq_Account request)
        {
            var res = new ResponseData<MRes_Account>();
            try
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Username == request.Username && x.PassWord == request.PassWord);

                if (user == null)
                {
                    res.error.message = "Sai tên đăng nhập hoặc mật khẩu!";
                    return res;
                }

                res.result = 1;
                res.data = new MRes_Account
                {
                    Token = GenerateToken(user)
                };
            }
            catch (Exception ex) 
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res ;
        }

        private string GenerateToken(Account user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim("UserName", user.Username),   
                new Claim("EmployeeId", user.EmployeeId.ToString()),    
                new Claim("RoleId", user.RoleId.ToString()),
                //roles

                new Claim("TokenId", Guid.NewGuid().ToString()),
            }),

                // thời gian hết hạn
                Expires = DateTime.UtcNow.AddHours(1),

                // ký
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha256Signature)
            };

            // token
            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }

        public Dictionary<string, string> DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var claims = principal.Claims.ToDictionary(x => x.Type, x => x.Value);

                return new Dictionary<string, string>
                {
                    { "UserName", claims.ContainsKey("UserName") ? claims["UserName"] : null },
                    { "EmployeeId", claims.ContainsKey("EmployeeId") ? claims["EmployeeId"] : null },
                    { "RoleId", claims.ContainsKey("RoleId") ? claims["RoleId"] : null }
                };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, string> { { "Error", $"Invalid token: {ex.Message}" } };
            }
        }


        // google drive
        public async Task<string> UploadFileAsync(IFormFile file, string folderId = "1BBNuAiuaJLofZAkoKWmFkPCfbNyVrpwc")
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

        public async Task<ResponseData<string>> ChangeAvatar (IFormFile file , string user_name)
        {
            var res = new ResponseData<string>();
            try
            {
                if(file == null || file.Length == 0)
                {
                    res.error.message = "File không hợp lệ!";
                    return res;
                }
                var user = _context.Accounts.FirstOrDefault(x => x.Username == user_name);
                if (user == null) 
                {
                    res.error.message = "Không tìm thấy Account!";
                    return res;
                }
                var avatar_url = await UploadFileAsync(file, "1BBNuAiuaJLofZAkoKWmFkPCfbNyVrpwc");

                // kiểm tra avatar_url ngay đây

                if (avatar_url == null)
                {
                    res.error.code = 400;
                    res.error.message = "Thay đổi avatar thất bại!";
                    return res;
                }

                user.AvatarUrl = avatar_url;

                _context.Accounts.Update(user);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                res.data = avatar_url;
                res.result = 1;
                res.error.message = "Thay đổi avatar thành công!";
            } catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res;
        }
    }
}
