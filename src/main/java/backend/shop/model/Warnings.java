@NoArgsConstructor
@AllArgsConstructor
@Getters
@Setters
@Entity
public class Warnings{
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;
    private String description;
    private Date recivedDate;
    private boolean isReady;
    private String type; //warning, error itp
    //połącz klucz
    private int orderId;
    
}