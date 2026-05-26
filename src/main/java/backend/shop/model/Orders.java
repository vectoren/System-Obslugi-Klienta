package backend.shop.model;

import jakarta.persistence.*;
import lombok.*;

import java.math.BigDecimal;
import java.util.Date;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Entity
public class Orders{
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer orderId;
    private String products;
    private BigDecimal wholeCost;
    private Date orderDate;

    @OneToOne(mappedBy = "orderId", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private PaymentDetails paymentDetails;
    @OneToOne(mappedBy = "orderId", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private DeliveryDetails deliveryDetails;

}
