using API_Sample.Data.EF;
using API_Sample.Data.Entities;
using API_Sample.Models.Common;
using API_Sample.Models.Request;
using API_Sample.Models.Response;
using API_Sample.Utilities;
using API_Sample.Utilities.Constants;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.String;

namespace API_Sample.Application.Services
{
    public interface IS_Product
    {
        Task<ResponseData<MRes_Product>> Create(MReq_Product request);

        Task<ResponseData<MRes_Product>> Update(MReq_Product request);

        Task<ResponseData<MRes_Product>> UpdateStatus(int id, short status, int updatedBy);

        Task<ResponseData<List<MRes_Product>>> UpdateStatusList(string sequenceIds, short status, int updatedBy);

        Task<ResponseData<int>> Delete(int id);

        Task<ResponseData<MRes_Product>> GetById(int id);

        Task<ResponseData<List<MRes_Product>>> GetListByStatus(int status);

        Task<ResponseData<List<MRes_Product>>> GetListByPaging(MReq_ProductPaging request);

        Task<ResponseData<List<MRes_Product>>> GetListBySequenceStatusSearchText(string sequenceStatus, string searchText);
    }

    public class S_Product : IS_Product
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public S_Product(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseData<MRes_Product>> Create(MReq_Product request)
        {
            var res = new ResponseData<MRes_Product>();
            try
            {
                request.Code = request.Code.ToUpper().Trim();
                var isExistsCode = await _context.Products.FirstOrDefaultAsync(x => x.Code == request.Code && x.Status != -1) != null;
                if (isExistsCode)
                {
                    res.error.message = "Mã trùng lặp!";
                    return res;
                }

                var data = new Product();
                data.Id = _context.Products.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() + 1;
                data.Name = request.Name;
                data.NameSlug = StringHelper.ToUrlClean(request.Name);
                data.Code = request.Code.ToUpper().Trim();
                data.Sort = request.Sort ?? 0;
                data.RatioTransfer = request.RatioTransfer;
                data.Remark = request.Remark;
                data.Status = request.Status;
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = request.CreatedBy;
                _context.Products.Add(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_CREATE;
                    return res;
                }
                var getById = await GetById(data.Id);
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

        public async Task<ResponseData<MRes_Product>> Update(MReq_Product request)
        {
            var res = new ResponseData<MRes_Product>();
            try
            {
                request.Code = request.Code.ToUpper().Trim();
                var isExistsCode = await _context.Products.FirstOrDefaultAsync(x => x.Code == request.Code && x.Status != -1 && x.Id != request.Id) != null;
                if (isExistsCode)
                {
                    res.error.message = "Mã trùng lặp!";
                    return res;
                }

                var data = await _context.Products.FindAsync(request.Id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                data.Name = request.Name;
                data.NameSlug = StringHelper.ToUrlClean(request.Name);
                data.Code = request.Code.ToUpper().Trim();
                data.Sort = request.Sort ?? 0;
                data.RatioTransfer = request.RatioTransfer;
                data.Remark = request.Remark;
                data.Status = request.Status;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = request.UpdatedBy;
                _context.Update(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var getById = await GetById(data.Id);
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

        public async Task<ResponseData<MRes_Product>> UpdateStatus(int id, short status, int updatedBy)
        {
            var res = new ResponseData<MRes_Product>();
            try
            {
                var data = await _context.Products.FindAsync(id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                data.Status = status;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = updatedBy;
                _context.Update(data);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                var getById = await GetById(data.Id);
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

        public async Task<ResponseData<List<MRes_Product>>> UpdateStatusList(string sequenceIds, short status, int updatedBy)
        {
            var res = new ResponseData<List<MRes_Product>>();
            try
            {
                List<int> ids = JsonConvert.DeserializeObject<List<int>>(sequenceIds);
                if (!ids.Any())
                {
                    res.error.code = 0;
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                var datas = await _context.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
                if (!datas.Any())
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                datas.ForEach(x =>
                {
                    x.Status = status;
                    x.UpdatedBy = updatedBy;
                    x.UpdatedAt = DateTime.Now;
                });

                _context.Products.UpdateRange(datas);
                var save = await _context.SaveChangesAsync();
                if (save == 0)
                {
                    res.error.code = 400;
                    res.error.message = MessageErrorConstants.EXCEPTION_DO_NOT_UPDATE;
                    return res;
                }
                res.data = _mapper.Map<List<MRes_Product>>(datas);
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

        public async Task<ResponseData<int>> Delete(int id)
        {
            var res = new ResponseData<int>();
            try
            {
                var data = await _context.Products.FindAsync(id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }

                _context.Products.Remove(data);
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

        public async Task<ResponseData<MRes_Product>> GetById(int id)
        {
            var res = new ResponseData<MRes_Product>();
            try
            {
                var data = await _context.Products.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
                    return res;
                }
                var mapData = _mapper.Map<MRes_Product>(data);
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

        public async Task<ResponseData<List<MRes_Product>>> GetListByStatus(int status)
        {
            var res = new ResponseData<List<MRes_Product>>();
            try
            {
                var data = await _context.Products.AsNoTracking().Where(x => x.Status == status).OrderBy(x => x.Sort).ToListAsync();
                res.data = _mapper.Map<List<MRes_Product>>(data);
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

        public async Task<ResponseData<List<MRes_Product>>> GetListByPaging(MReq_ProductPaging request)
        {
            var res = new ResponseData<List<MRes_Product>>();
            try
            {
                List<short> status = request.SequenceStatus.Split(',')?.Select(short.Parse).ToList();
                var query = _context.Products.AsNoTracking().Where(x => (status.Count == 1 ? x.Status == status[0] : status.Contains(x.Status)));

                if (!IsNullOrEmpty(request.SearchText))
                {
                    request.SearchText = request.SearchText.Trim();
                    var searchTextClean = StringHelper.ToUrlClean(request.SearchText);
                    query = query.Where(x => x.NameSlug.StartsWith(searchTextClean) || x.Code.StartsWith(request.SearchText));
                }

                var totalRecords = await query.CountAsync();
                var data = await query.OrderBy(x => x.Sort).Skip((request.Page - 1) * request.Record).Take(request.Record).ToListAsync();

                res.data = _mapper.Map<List<MRes_Product>>(data);
                res.data2nd = totalRecords;
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

        public async Task<ResponseData<List<MRes_Product>>> GetListBySequenceStatusSearchText(string sequenceStatus, string searchText)
        {
            var res = new ResponseData<List<MRes_Product>>();
            try
            {
                List<short> status = sequenceStatus.Split(',')?.Select(short.Parse).ToList();
                var query = _context.Products.AsNoTracking().Where(x => (status.Count == 1 ? x.Status == status[0] : status.Contains(x.Status)));

                if (!IsNullOrEmpty(searchText))
                {
                    searchText = searchText.Trim();
                    var searchTextClean = StringHelper.ToUrlClean(searchText);
                    query = query.Where(x => x.NameSlug.StartsWith(searchTextClean) || x.Code.StartsWith(searchText));
                }

                var data = await query.OrderBy(x => x.Sort).ToListAsync();
                res.data = _mapper.Map<List<MRes_Product>>(data);
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
    }
}