package backend.shop.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.*;

import java.math.BigDecimal;
import java.util.Date;
import java.util.Map;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
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
