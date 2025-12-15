using FluentValidation;

namespace FU_House_Finder.DTO.Validators
{
    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(x => x.HouseId)
                .GreaterThan(0)
                .WithMessage("House ID phải lớn hơn 0");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên phòng không được để trống")
                .MinimumLength(3)
                .WithMessage("Tên phòng phải có ít nhất 3 ký tự")
                .MaximumLength(100)
                .WithMessage("Tên phòng không được vượt quá 100 ký tự");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Giá phòng phải lớn hơn 0");

            RuleFor(x => x.Area)
                .GreaterThan(0)
                .WithMessage("Diện tích phòng phải lớn hơn 0");

            RuleFor(x => x.MaxPeople)
                .GreaterThan(0)
                .WithMessage("Số người tối đa phải lớn hơn 0")
                .LessThanOrEqualTo(10)
                .WithMessage("Số người tối đa không được vượt quá 10");

            RuleFor(x => x.Status)
                .InclusiveBetween(0, 1)
                .WithMessage("Trạng thái phòng phải là 0 (Available) hoặc 1 (Rented)");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Mô tả không được vượt quá 500 ký tự");
        }
    }
}