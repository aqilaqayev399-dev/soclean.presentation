using soclean.core.Entities;
using soclean.dataccess.Contex;
using soclean.dataccess.Repositories.Abstract;
using soclean.dataccess.Repositories.Implementations.Generic;

namespace soclean.dataccess.Repositories.Implementations;

internal class SliderRepository : Repository<Slider>, ISliderRepository
{
    public SliderRepository(AppDbContext context) : base(context)
    {
    }
}
