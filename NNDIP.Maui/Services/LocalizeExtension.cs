using Microsoft.Extensions.Localization;
using NNDIP.Maui.Resources.Languages;

namespace NNDIP.Maui.Services
{
    [ContentProperty(nameof(Key))]
    public class LocalizeExtension : IMarkupExtension
    {
        IStringLocalizer<AppResources> _localizer;

        public string Key { get; set; } = string.Empty;

        public LocalizeExtension()
        {
            _localizer = ServiceHelper.GetService<IStringLocalizer<AppResources>>();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            string localizedText = _localizer[Key];
            return localizedText;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}
