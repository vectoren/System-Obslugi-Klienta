@NoArgsConstructor
@AllArgsConstructor
@Getters
@Setters
@Entity
public class Orders{
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;
    private Map<Products, Integer> products;
    private BigDecimal wholeCost;
    private Date orderDate;

    //foreign keys to connect
    private int paymentDetails;
    private int deliveryDetails;

}