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
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Application.Services
{
    public interface IS_NewsCategory {
        Task<ResponseData<MRes_NewsCategory>> Create(MReq_NewsCategory request);

        Task<ResponseData<MRes_NewsCategory>> Update(MReq_NewsCategory request);

        Task<ResponseData<MRes_NewsCategory>> UpdateStatus(int id, bool status, string updatedBy);

        Task<ResponseData<int>> Delete(int id);

        Task<ResponseData<MRes_NewsCategory>> GetById(int id);

        Task<ResponseData<List<MRes_NewsCategory>>> GetListByStatus(bool status);
    }
    public class S_NewsCategory : IS_NewsCategory
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_NewsCategory (MainDbContext conetxt, IMapper mapper)
        {
            _context = conetxt;
            _mapper = mapper;
        }
        public async Task<ResponseData<MRes_NewsCategory>> Create(MReq_NewsCategory request)
        {
            var res = new ResponseData<MRes_NewsCategory>();
            try
            {
                var data = new NewsCategory();
                data.NewsCategoryName = request.NewsCategoryName;
                data.Status = request.IsActive;
                data.CreateAt = DateTime.Now;
                data.CreateBy = request.CreateBy;

                _context.NewsCategories.Add(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_CREATE;
                    return res;
                }
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
                var data = await _context.NewsCategories.FindAsync(id);
                if (data == null)
                {
                    res.error.message = "Không tìm thấy Thể loại tin tức!";
                    return res;
                }
                _context.NewsCategories.Remove(data);
                var save = await _context.SaveChangesAsync();

                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_DELETE;
                    return res;
                }
                res.data = data.NewsCategoryId;
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

        public async Task<ResponseData<MRes_NewsCategory>> GetById(int id)
        {
            var res = new ResponseData<MRes_NewsCategory>();
            try
            {
                var data = await _context.NewsCategories.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.NewsCategoryId == id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                var mapData = _mapper.Map<MRes_NewsCategory>(data);
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

        public async Task<ResponseData<List<MRes_NewsCategory>>> GetListByStatus(bool status)
        {
            var res = new ResponseData<List<MRes_NewsCategory>>();
            try
            {
                var data = await _context.NewsCategories.AsNoTracking().Where(x => x.Status == status).OrderBy(x => x.NewsCategoryName).ToListAsync();
                res.data = _mapper.Map<List<MRes_NewsCategory>>(data);
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

        public async Task<ResponseData<MRes_NewsCategory>> Update(MReq_NewsCategory request)
        {
            var res = new ResponseData<MRes_NewsCategory>();
            try
            {
                var data = await _context.NewsCategories.FindAsync(request.NewsCategoryId);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                data.NewsCategoryName = request.NewsCategoryName;
                data.Status = request.IsActive;
                data.UpdateAt = DateTime.Now;
                data.UpdateBy = request.UpdateBy;

                var save = _context.SaveChanges();

                if (save == 0)
                {
                    res.error.message = "";
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var mapdata = _mapper.Map<MRes_NewsCategory>(data);
                res.data = mapdata;
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

        public async Task<ResponseData<MRes_NewsCategory>> UpdateStatus(int id, bool status, string updatedBy)
        {
            var res = new ResponseData<MRes_NewsCategory>();
            try
            {
                var data = await _context.NewsCategories.FindAsync(id);
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
                var getById = await GetById(data.NewsCategoryId);
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
