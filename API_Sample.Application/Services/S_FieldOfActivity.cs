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
using SQLitePCL;

namespace API_Sample.Application.Services
{
    public interface IS_FieldOfActivity
    {
        Task<ResponseData<MRes_FieldOfActivity>> Create(MReq_FieldOfActivity request);

        Task<ResponseData<MRes_FieldOfActivity>> Update(MReq_FieldOfActivity request);

        Task<ResponseData<MRes_FieldOfActivity>> UpdateStatus(int id, int status, string updatedBy);

        Task<ResponseData<int>> Delete(int id);

        Task<ResponseData<MRes_FieldOfActivity>> GetById(int id);

        public Task<ResponseData<List<MRes_FieldOfActivity>>> GetListByStatus(int status);
    }
    public class S_FieldOfActivity : IS_FieldOfActivity
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_FieldOfActivity(MainDbContext conetxt, IMapper mapper) 
        {
            _context = conetxt;
            _mapper = mapper;
        }
        public async Task<ResponseData<MRes_FieldOfActivity>> Create(MReq_FieldOfActivity request)
        {
            var res = new ResponseData<MRes_FieldOfActivity>();
            try
            {
                //var exitsField = await _context.FieldOfActivities.FirstOrDefaultAsync(x => x.FieldId == request.FieldId) != null;

                //if (exitsField)
                //{
                //    res.error.message = "Mã lĩnh vực trùng lặp!";
                //    return res;
                //}

                var data = new FieldOfActivity();
                data.FieldName = request.FieldName;
                data.Status = request.IsActive;
                data.CreateAt = DateTime.Now;
                data.CreateBy = request.CreateBy;

                _context.FieldOfActivities.Add(data);
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
                var data = await _context.FieldOfActivities.FindAsync(id);
                if (data == null)
                {
                    res.error.message = "Không tìm thấy lĩnh vực hoạt động!";
                    return res;
                }
                _context.FieldOfActivities.Remove(data);
                var save = await _context.SaveChangesAsync();

                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_DELETE;
                    return res;
                }
                res.data = data.FieldId;
                res.result = 1;
                res.error.message = MessageErrorConstants.DELETE_SUCCESS;
            }
            catch (Exception ex)
            {
                res.result = -1;
                res.error.code = 500;
                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
            }
            return res ;
        }

        public async Task<ResponseData<MRes_FieldOfActivity>> GetById(int id)
        {
            var res = new ResponseData<MRes_FieldOfActivity>();
            try
            {
                var data = await _context.FieldOfActivities.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.FieldId == id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                var mapData = _mapper.Map<MRes_FieldOfActivity>(data);
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

        public async Task<ResponseData<List<MRes_FieldOfActivity>>> GetListByStatus(int status)
        {
            var res = new ResponseData<List<MRes_FieldOfActivity>>();
            try
            {
                var data = await _context.FieldOfActivities.AsNoTracking().Where(x => x.Status == status).OrderBy(x => x.FieldName).ToListAsync();
                res.data = _mapper.Map<List<MRes_FieldOfActivity>>(data);
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

        public async Task<ResponseData<MRes_FieldOfActivity>> Update(MReq_FieldOfActivity request)
        {
            var res = new ResponseData<MRes_FieldOfActivity>();
            try
            {
                //var exitsField = await _context.FieldOfActivities.FirstOrDefaultAsync(x => x.FieldId == request.FieldId) != null;
                //if(exitsField)
                //{
                //    res.error.message = "Mã trùng lặp!";
                //    return res;
                //}
                var data = await _context.FieldOfActivities.FindAsync(request.FieldId);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                data.FieldName = request.FieldName;
                data.Status = request.IsActive;
                data.UpdateAt = DateTime.Now;
                data.UpdateBy = request.UpdateBy;

                var save = _context.SaveChanges();

                if(save == 0)
                {
                    res.error.message = "";
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var mapdata =  _mapper.Map<MRes_FieldOfActivity>(data);
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

        public async Task<ResponseData<MRes_FieldOfActivity>> UpdateStatus(int id, int status, string updatedBy)
        {
            var res = new ResponseData<MRes_FieldOfActivity>();
            try
            {
                var data = await _context.FieldOfActivities.FindAsync(id);
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
                var getById = await GetById(data.FieldId);
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
