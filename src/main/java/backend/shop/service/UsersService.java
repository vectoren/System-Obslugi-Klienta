package backend.shop.service;

import backend.shop.model.Users;
import backend.shop.repo.UsersRepo;

public class UsersService {
    private UsersRepo repo;

    public UsersService(UsersRepo repo){
        this.repo = repo;
    }

    public boolean registerUser(Users user){
        try{
            var x = repo.save(user);
            return true;

        }
        catch (Exception ex){
            return false;
        }
    }
}
