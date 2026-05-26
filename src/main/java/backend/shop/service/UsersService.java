package backend.shop.service;

import backend.shop.model.Users;
import backend.shop.repo.UsersRepo;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class UsersService {
    private final UsersRepo repo;

    public UsersService(UsersRepo repo){
        this.repo = repo;
    }

    public boolean registerUser(Users user){
        try{
            user.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
            var x = repo.save(user);
            return true;

        }
        catch (Exception ex){
            return false;
        }
    }
}
