using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Services;

public static class MapsterConfiguration
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        /* TypeAdapterConfig<Faaliyet, FaaliyetResponseDto>.NewConfig()
        .Map(dest => dest.PersonelSayisi, src => src.FaaliyetPersonel.Count());

        TypeAdapterConfig<BirimTurKkyNesneTur, KkyNesneTurDto>.NewConfig()
        .Map(dest => dest.NesneTurId, src => src.KkyNesneTur.NesneTurId)
        .Map(dest => dest.TeknikBirimNesneTur, src => src.KkyNesneTur.TeknikBirimNesneTur)
        .Map(dest => dest.TeknikBirimNesneTurTanim, src => src.KkyNesneTur.TeknikBirimNesneTurTanim);

        TypeAdapterConfig<KullaniciCihazDto, KullaniciCihaz>.NewConfig()
            .Map(dest => dest.SonGirisTarihi, src => DateTime.Now); */
    }
}
