namespace Framework.Application
{
    public static class ValidationMessages
    {
        public const string RequiredMessage = "پر کردن این فیلد الزامی است";
        public const string NotInRangeMessage = "عدد وارد شده در محدود مجاز نیست";
        public const string InvalidModelStateMessage = "اطلاعات فرم وارد شده صحیح نیست, لطفا مجددا امتحان کنید";
        public const string PasswordsMismatchMessage = "عدم تطابق دو رمز وارد شده";
        public const string MaxFileSizeMessage = "سایز فایل نباید بیشتر از 12 مگابایت باشد";
        public const string FileExtensionNotAllowed = "پسوند این فایل همخوانی ندارد ";
        public const string PhoneNumberIncorrect = "مانند: 09123456789";
        public const string NameIncorrect = "نام وارد شده صحیح نمی باشد";
    }
}
