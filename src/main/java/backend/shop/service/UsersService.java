package backend.shop.service;

import backend.shop.model.Users;
import backend.shop.repo.UsersRepo;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class UsersService {
    private final UsersRepo repo;

    public UsersService(UsersRepo repo){
        this.repo = repo;
    }

    public Optional<Users> registerUser(Users user){
        try{
            Users userCopy = new Users(user);
            user.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
            repo.save(user);
            return Optional.of(userCopy);

        }
        catch (Exception ex){
            return Optional.empty();
        }
    }
}
