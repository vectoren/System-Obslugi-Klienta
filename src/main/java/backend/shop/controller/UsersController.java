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

        if(x.isPresent()){
            return new ResponseEntity<>(x.get(), HttpStatusCode.valueOf(200));
        }
        return new ResponseEntity<>("User created unsuccessfuly", HttpStatus.BAD_REQUEST);
    }
    @GetMapping("/{id}/forgot-password")
    public ResponseEntity<?> forgotPassword(@PathVariable int id){
        var usersPassword = this.usersService.resetPassword(id);
        if(usersPassword.isPresent()){
            return new ResponseEntity<>(usersPassword.get(), HttpStatusCode.valueOf(200));
        }
        return new ResponseEntity<>("Cos poszło nie tak", HttpStatus.valueOf(400));
    }

    @DeleteMapping("/{id}/delete")
    public ResponseEntity<?> deleteUser(@PathVariable int id){
        if(this.usersService.deleteUser(id)){
            return new ResponseEntity<>("User deleted Successfuly", HttpStatusCode.valueOf(204));
        }
        return new ResponseEntity<>("User deleted unsuccessfuly", HttpStatusCode.valueOf(400));
    }

    @PutMapping("/{id}/update")
    public ResponseEntity<?> updateUser(@PathVariable int id, @RequestBody Users user){
        var updatedUser = this.usersService.updateUser(id, user);
        if(updatedUser.isPresent()){
            return new ResponseEntity<>(updatedUser.get(), HttpStatusCode.valueOf(200));
        }
        return new ResponseEntity<>("Something went wrong", HttpStatusCode.valueOf(400));
    }

}