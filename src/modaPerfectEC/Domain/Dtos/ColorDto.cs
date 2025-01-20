using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class ColorDto
{
    public string Color { get; set; }
    public string Hex {  get; set; }
    public int StockAmount { get; set; }

    public ColorDto()
    {
        Color = string.Empty;
        Hex = string.Empty;
    }

    public ColorDto(string color, string hex)
    {
        Color = color;
        Hex = hex;
    }
}
