using GuiApp.Business.Modules.Sample.DTOs;
using GuiApp.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace GuiApp.Business.Modules.Sample.DomainServices;

public class FlatUIColorPicker
{
    private readonly ILogger _logger;

    public FlatUIColorPicker(ILogger logger)
    {
        _logger = logger;
    }

    public IEnumerable<FlatColorDto> GetFlatColors()
    {
        return EmbeddedDataAccess.ReadFlatColors(_logger)
            .Select(entity => new FlatColorDto() { Hex = entity.Hex, Name = entity.Name });
    }
}
