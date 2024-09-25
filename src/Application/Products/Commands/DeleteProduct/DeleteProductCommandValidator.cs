using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Features.ProductFeatures.Command;
public class DeleteProductCommandValidator : AbstractValidator<DeleteProduct>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is not empty");
    }
}
