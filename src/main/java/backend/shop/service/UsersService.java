package backend.shop.service;

import backend.shop.model.Users;
import backend.shop.repo.UsersRepo;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.List;

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

    public Optional<Users> resetPassword(String email){
        try{
            Optional<Users> user = this.repo.getByEmail(email);

            if(user.isPresent()){
                return user;
            }
            throw new Exception("User Not found");
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return Optional.empty();
        }
    }

    public boolean setActive(int id){
        try{
            Optional<Users> userFound = this.repo.findById(id);
            if(userFound.isPresent()) {
                Users u = userFound.get();
                u.setActive(true);
                this.repo.save(u);
                return true;
            }
            throw new Exception("user Not found");
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return false;
        }
    }

    public Optional<List<Users>> getActiveAdmins() {
        try{
            List<Users> activeAdmins = this.repo.findAllActiveByRoleWithRoles("ADMIN");
            if(!activeAdmins.isEmpty()){
                return Optional.of(activeAdmins);
            }
            throw new Exception("Blad");
        }
        catch (Exception e) {
            System.out.println(e.getMessage());
            return Optional.empty();
        }
    }
}
