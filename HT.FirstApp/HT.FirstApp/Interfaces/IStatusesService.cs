﻿using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IStatusesService
    {
        Task<ResponseMessage> GetStatusesAsync(int position, int pageIndex = 1, int pageSize = 20);
        Task<ResponseMessage> EditStatusesAsync(Statuses statuses);
        Task<ResponseMessage> DeleteStatusesAsync(int id);
    }
}
