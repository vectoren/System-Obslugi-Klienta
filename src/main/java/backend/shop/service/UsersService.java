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

    public boolean deleteUser(int id){
        try{
            this.repo.deleteById(id);
            return true;
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return false;
        }
    }

    public Optional<Users> updateUser(int id, Users user) {
        try{
            Optional<Users> u = this.repo.findById(id);
            if(u.isPresent()){
                Users selectedUser = u.get();
                selectedUser.setFirstName(user.getFirstName());
                selectedUser.setLastName(user.getLastName());
                selectedUser.setEmail(user.getEmail());
                selectedUser.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
                selectedUser.setRole(user.getRole());

                System.out.println(selectedUser.toString());
                this.repo.save(selectedUser);
                return Optional.of(selectedUser);
            }
            throw new Exception("User not found");
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return Optional.empty();
        }
    }

    public Optional<String> resetPassword(int id){
        try{
            Optional<Users> user = this.repo.findById(id);
            String newPassword = generateNewPassword();
            if(user.isPresent()){
                var realUser = user.get();
                realUser.setPassword(new BCryptPasswordEncoder().encode(newPassword));
                this.repo.save(realUser);
            }
            return Optional.of(newPassword);
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return Optional.empty();
        }
    }

    private String generateNewPassword(){
        StringBuilder sb = new StringBuilder();
        char randomAscii;
        for(int i = 0; i < 10; i++){
            randomAscii = (char)((int)(Math.random() * 127));
            sb.append(randomAscii);
        }
        return sb.toString();
    }
}
