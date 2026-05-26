package backend.shop.service;

import backend.shop.model.UserProfiler;
import backend.shop.model.Users;
import backend.shop.repo.UsersRepo;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class UserProfilerService implements UserDetailsService {

    private final UsersRepo repo;

    public UserProfilerService(UsersRepo repo){
        this.repo = repo;
    }

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        Optional<Users> user = repo.getByEmail(username);
        if(user.isPresent()){
            return new UserProfiler(user.get());
        }
        throw new UsernameNotFoundException("Cos nie dziala");

    }
}
