package backend.shop.repo;

import backend.shop.model.Users;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UsersRepo extends JpaRepository<Users, Integer> {
    String getByEmail(String email);
}