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
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Application.Services
{
    public interface IS_Post
    {
        Task<ResponseData<MRes_Post>> Create(MReq_Post request);

        Task<ResponseData<MRes_Post>> Update(MReq_Post request);

        Task<ResponseData<MRes_Post>> UpdateStatus(int post_id, bool status, int updatedBy);

        //Task<ResponseData<List<MRes_Post>>> UpdateStatusList(string sequenceIds, short status, int updatedBy);

        Task<ResponseData<int>> Delete(int id);

        Task<ResponseData<MRes_Post>> GetById(int id);

        Task<ResponseData<List<MRes_Post>>> GetListByStatus(bool status);

        Task<ResponseData<List<MRes_Post>>> GetListByPaging(MReq_PostPaging request);

        Task<ResponseData<List<MRes_Post>>> GetListBySequenceStatusSearchText(string sequenceStatus, string searchText);
    }
    public class S_Post : IS_Post
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_Post(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseData<MRes_Post>> Create(MReq_Post request)
        {
            var res = new ResponseData<MRes_Post>();
            try
            {
                var isExistsCode = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == request.PostId) != null;
                if (isExistsCode)
                {
                    res.error.message = "Mã trùng lặp!";
                    return res;
                }

                var data = new Post();
                data.PostId = _context.Posts.OrderByDescending(x => x.PostId).Select(x => x.PostId).FirstOrDefault() + 1;
                data.Title = request.Title;
                data.NameSlug = StringHelper.ToUrlClean(request.Title.Substring(0,10));
                data.ShortDescription = request.ShortDescription;
                data.Content = request.Content;
                data.FieldId = request.FieldId;
                data.ViewCount = request.ViewCount;
                data.Status = request.IsActive;
                data.Username = request.Username;
                _context.Posts.Add(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_CREATE;
                    return res;
                }
                var getById = await GetById(data.PostId);
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
                var data = await _context.Posts.FindAsync(id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                _context.Posts.Remove(data);
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

        public async Task<ResponseData<MRes_Post>> GetById(int post_id)
        {
            var res = new ResponseData<MRes_Post>();
            try
            {
                var data = await _context.Posts.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.PostId == post_id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                var mapData = _mapper.Map<MRes_Post>(data);
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

        public Task<ResponseData<List<MRes_Post>>> GetListByPaging(MReq_PostPaging request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<List<MRes_Post>>> GetListBySequenceStatusSearchText(string sequenceStatus, string searchText)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<List<MRes_Post>>> GetListByStatus(bool status)
        {
            var res = new ResponseData<List<MRes_Post>>();
            try
            {
                var data = await _context.Posts.AsNoTracking().Where(x => x.Status == status).OrderBy(x => x.PostId).ToListAsync();
                res.data = _mapper.Map<List<MRes_Post>>(data);
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

        public async Task<ResponseData<MRes_Post>> Update(MReq_Post request)
        {
            var res = new ResponseData<MRes_Post>();
            try
            {
                var isExistsCode = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == request.PostId) != null;
                if (isExistsCode)
                {
                    res.error.message = "Mã trùng lặp!";
                    return res;
                }

                var data = await _context.Posts.FindAsync(request.PostId);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                data.Title = request.Title;
                data.NameSlug = StringHelper.ToUrlClean(request.Title.Substring(0, 10));
                data.ShortDescription = request.ShortDescription;
                data.Content = request.Content;
                data.FieldId = request.FieldId;
                data.ViewCount = request.ViewCount;
                data.Status = request.IsActive;
                data.Username = request.Username;
                _context.Update(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var getById = await GetById(data.PostId);
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

        public async Task<ResponseData<MRes_Post>> UpdateStatus(int post_id, bool status, int updatedBy)
        {
            var res = new ResponseData<MRes_Post>();
            try
            {
                var data = await _context.Posts.FindAsync(post_id);
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
                var getById = await GetById(data.PostId);
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
    }
}
