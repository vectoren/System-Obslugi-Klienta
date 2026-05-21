@NoArgsConstructor
@AllArgsConstructor
@Getters
@Setters
@Entity
public class Products{

    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;
    private String productName;
    private int amount;
    private BigDecimal price;
    private String description;
    private String category;
    private String imageUrl;
    
}