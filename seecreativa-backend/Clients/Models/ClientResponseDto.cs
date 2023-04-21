namespace seecreativa_backend.Clients.Models {
    public class ClientResponseDto {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? RFC { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
