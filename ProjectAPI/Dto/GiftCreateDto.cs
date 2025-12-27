namespace ProjectAPI.Dto
{
    public class GiftCreateDto
    {
        
            public string Name { get; set; }
            public int GiftNumber { get; set; }
            public int IdDonor { get; set; }      
            public int IdCatgory { get; set; }    
            public int Price { get; set; }
            public string PathImage { get; set; }
        

    }
}
