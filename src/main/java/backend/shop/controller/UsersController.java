package backend.shop.controller;

import backend.shop.model.Users;
import backend.shop.service.UsersService;
import org.springframework.http.HttpStatus;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api")
@CrossOrigin
public class UsersController{

    private final UsersService usersService;
    public UsersController(UsersService us){
        this.usersService = us;
    }

    @PostMapping("/register")
    public ResponseEntity<?> registerUser(@RequestBody Users userData){
        var x = this.usersService.registerUser(userData);

        if(x){
            return new ResponseEntity<>("User created successfuly", HttpStatusCode.valueOf(200));
        }
        return new ResponseEntity<>("User created unsuccessfuly", HttpStatus.BAD_REQUEST);
        
    }

}