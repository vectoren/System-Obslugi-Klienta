@NoArgsConstructor
@AllArgsConstructor
@Getters
@Setters
@Entity
public class DeliveryDetails{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    private String region; //Woj
    private String city;//miasto
    private String street;//ulica
    private String townCode;//kod pocztowy
    private String homeNumber;//nr domu
    private Date deliveredDate;
    private double deliveryCost;
}
