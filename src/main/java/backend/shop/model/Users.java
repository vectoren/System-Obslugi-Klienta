@NoArgsConstructor
@AllArgsConstructor
@Getters
@Setters
@Entity
// dodaj index na email + dodaj not null'e
public class Users{

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    private String firstName;
    private String lastName;
    private String email;
    private String password;
    private Set<String> role;
    private Date accountCreationDate;

    //klucz do deliveryDetails;
    private int deliveryDetails;
}