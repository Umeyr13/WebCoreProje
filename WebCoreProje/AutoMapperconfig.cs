using AutoMapper;
using WebCoreProje.Models.Entities;
using WebCoreProje.Models.ViewModel;

namespace WebCoreProje
{
    public class AutoMapperconfig:Profile
    {
        public AutoMapperconfig()
        {
            CreateMap<User,UserModel>().ReverseMap();//User ı UserModele Dönüştür .Reverse ise tersini de yap yani usermodeli user a dönüştür.

            CreateMap<User,CreateUserModel>().ReverseMap();
            CreateMap<User,EditUserModel>().ReverseMap();
        }
    }
}
