using FluentValidation;

namespace FU_House_Finder.DTO.Validators
{
    public class UpdateHouseDtoValidator : AbstractValidator<UpdateHouseDto>
    {
        public UpdateHouseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên nhà trọ không được để trống")
                .MinimumLength(3)
                .WithMessage("Tên nhà trọ phải có ít nhất 3 ký tự")
                .MaximumLength(100)
                .WithMessage("Tên nhà trọ không được vượt quá 100 ký tự");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Địa chỉ không được để trống")
                .MinimumLength(5)
                .WithMessage("Địa chỉ phải có ít nhất 5 ký tự")
                .MaximumLength(200)
                .WithMessage("Địa chỉ không được vượt quá 200 ký tự");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Mô tả không được vượt quá 500 ký tự");

            RuleFor(x => x.CampusName)
                .NotEmpty()
                .WithMessage("Tên khu vực/campus không được để trống")
                .MaximumLength(100)
                .WithMessage("Tên khu vực không được vượt quá 100 ký tự");

            RuleFor(x => x.PowerPrice)
                .GreaterThan(0)
                .WithMessage("Giá điện phải lớn hơn 0");

            RuleFor(x => x.WaterPrice)
                .GreaterThan(0)
                .WithMessage("Giá nước phải lớn hơn 0");
        }
    }
}