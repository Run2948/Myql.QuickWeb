
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
using QuickWeb.Areas.Admin.Models.ViewModel;

namespace QuickWeb
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(m =>
            {
                //m.CreateMap<UserInfoView, UserInfoOutputDto>();
                m.CreateMap<UserInfoOutputDto, UserInfoView>().ForMember(dest => dest.rule, opt => opt.Ignore());
                //m.CreateMap<RoleInfoView, snake_role>();
                m.CreateMap<snake_role, RoleInfoView>().ForMember(dest => dest.rule, opt => opt.Ignore());
                //m.CreateMap<NodeInfoView, snake_role>();
                m.CreateMap<snake_node, NodeInfoView>()
                    .ForMember(dest => dest.name, opt => opt.MapFrom(sc => sc.node_name))
                    .ForMember(dest => dest.pId, opt => opt.MapFrom(sc => sc.type_id))
                    .ForMember(dest => dest.@checked, opt => opt.Ignore());
                //m.CreateMap<NodeTreeView, snake_role>();
                m.CreateMap<snake_node, NodeTreeView>()
                    .ForMember(dest => dest.name, opt => opt.MapFrom(sc => sc.node_name))
                    .ForMember(dest => dest.pid, opt => opt.MapFrom(sc => sc.type_id))
                    .ForMember(dest => dest.children, opt => opt.Ignore());
            });
        }
    }
}