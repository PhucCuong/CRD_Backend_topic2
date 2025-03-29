//using AutoMapper;
//using API_Sample.Data.EF;
//using API_Sample.Models.Common;
//using API_Sample.Models.Response;
//using API_Sample.Utilities.Constants;
//using Microsoft.EntityFrameworkCore;

//namespace API_Sample.Application.Services
//{
//    public interface IS_Image
//    {
//        Task<ResponseData<MRes_Image>> GetById(int id);
//        Task<ResponseData<BaseModel.Image>> GetByIdCustomResponse(int id);
//        Task<ResponseData<List<MRes_Image>>> GetListByListId(List<int> ids);
//        Task DeleteImageOld(int? oldId, int? newId = 0);
//        Task DeleteListImage(List<int> ids);
//    }

//    public class S_Image : IS_Image
//    {
//        private readonly MainDbContext _context;
//        private readonly IMapper _mapper;

//        public S_Image(MainDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<ResponseData<MRes_Image>> GetById(int id)
//        {
//            var res = new ResponseData<MRes_Image>();
//            try
//            {
//                var data = await _context.Images.FindAsync(id);
//                if (data == null)
//                {
//                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
//                    return res;
//                }
//                res.data = _mapper.Map<MRes_Image>(data);
//                res.result = 1;
//            }
//            catch (Exception ex)
//            {
//                res.result = -1;
//                res.error.code = 500;
//                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
//            }
//            return res;
//        }

//        public async Task<ResponseData<BaseModel.Image>> GetByIdCustomResponse(int id)
//        {
//            var res = new ResponseData<BaseModel.Image>();
//            try
//            {
//                var data = await _context.Images.FindAsync(id);
//                if (data == null)
//                {
//                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
//                    return res;
//                }
//                res.data = _mapper.Map<BaseModel.Image>(data);
//                res.result = 1;
//            }
//            catch (Exception ex)
//            {
//                res.result = -1;
//                res.error.code = 500;
//                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
//            }
//            return res;
//        }

//        public async Task<ResponseData<List<MRes_Image>>> GetListByListId(List<int> ids)
//        {
//            var res = new ResponseData<List<MRes_Image>>();
//            try
//            {
//                var data = await _context.Images.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync();
//                if (data == null)
//                {
//                    res.error.message = MessageErrorConstants.DO_NOT_FIND_DATA;
//                    return res;
//                }
//                res.data = _mapper.Map<List<MRes_Image>>(data);
//                res.result = 1;
//            }
//            catch (Exception ex)
//            {
//                res.result = -1;
//                res.error.code = 500;
//                res.error.message = $"Exception: {ex.Message}\r\n{ex.InnerException?.Message}";
//            }
//            return res;
//        }

//        public async Task DeleteImageOld(int? oldId, int? newId = 0)
//        {
//            if (oldId > 0 && oldId != newId)
//            {
//                var data = await _context.Images.FindAsync(oldId);
//                if (data != null)
//                {
//                    data.Status = -1;
//                    data.Timer = DateTime.Now;
//                    _context.Images.Update(data);
//                    await _context.SaveChangesAsync();
//                }
//            }
//        }

//        public async Task DeleteListImage(List<int> ids)
//        {
//            if (ids != null && ids.Any())
//            {
//                ids = ids.Where(x => x != 0).ToList();
//                var data = await _context.Images.Where(x => ids.Contains(x.Id)).ToListAsync();
//                if (data != null)
//                {
//                    data.ForEach(x =>
//                    {
//                        x.Status = -1;
//                        x.Timer = DateTime.Now;
//                    });
//                    _context.Images.UpdateRange(data);
//                    await _context.SaveChangesAsync();
//                }
//            }
//        }
//    }
//}
