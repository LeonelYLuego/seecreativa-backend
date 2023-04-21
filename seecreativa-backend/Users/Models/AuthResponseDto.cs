namespace seecreativa_backend.Users.Models {
    public class AuthResponseDto {
        public required string Token { get; set; }
        public required UserResponseDto User { get; set; }
    }
}
