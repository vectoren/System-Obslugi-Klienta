
@RestController
@RequestMapping("/api")
@CrossOrigin
public class UsersController{

    private UsersService usersService;
    public UsersController(UsersService us){
        this.usersService = us;
    }

    PostMapping("/register")
    public ResponseEntity<String> registerUser(@RequestBody userData){
        var x = this.usersService.registerUser(userData);

        if(x){
            return new ResponseEntity<String>("User created successfuly", HttpStatusCode(200));
        }
        return new ResponseEntity<String>("User created unsuccessfuly", HttpStatus.BAD_REQUEST);
        
    }

}