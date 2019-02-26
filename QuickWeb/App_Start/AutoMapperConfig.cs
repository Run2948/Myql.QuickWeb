
/* ==============================================================================
* 命名空间：QuickWeb.App_Start 
* 类 名 称：AutoMapperConfig
* 创 建 者：Qing
* 创建时间：2019-02-26 13:36:59
* CLR 版本：4.0.30319.42000
* 保存的文件名：AutoMapperConfig
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using AutoMapper;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;

namespace QuickWeb
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(m =>
            {
                //m.CreateMap<UserInfo, UserInfoInputDto>();
                //m.CreateMap<UserInfoInputDto, UserInfo>();
                m.CreateMap<snake_user, UserInfoOutputDto>();
                m.CreateMap<UserInfoOutputDto, snake_user>();
                //m.CreateMap<UserInfoInputDto, UserInfoOutputDto>();
                //m.CreateMap<UserInfoOutputDto, UserInfoInputDto>();
            });
        }
    }
}