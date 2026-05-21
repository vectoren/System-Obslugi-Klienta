public interface UsersRepo extends JpaRepository<Integer, Users>{
    String getByEmail(String email);
}