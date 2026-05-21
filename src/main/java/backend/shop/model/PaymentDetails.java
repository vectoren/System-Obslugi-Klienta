@NoArgsConstructor
@AllArgsConstructor
@Getters
@Setters
@Entity
public class PaymentDetails{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    private Date paymentAccomplishedDate;
    private String paymentType;
    private boolean isPaid;
    

}