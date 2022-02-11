
using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public class CreateSlide
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string BtnText { get; set; }
        public string BtnColor { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Link { get; set; }

    }
}
