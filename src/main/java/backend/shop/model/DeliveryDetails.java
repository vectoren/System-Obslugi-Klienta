package backend.shop.model;

import jakarta.persistence.*;
import lombok.*;

import java.util.Date;


@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Entity
public class DeliveryDetails{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer deliveryDetailsId;
    private String region; //Woj
    private String city;//miasto
    private String street;//ulica
    private String townCode;//kod pocztowy
    private String homeNumber;//nr domu
    private Date deliveredDate;
    private double deliveryCost;

    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name ="userId")
    private Users userId;

    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "orderId")
    private Orders orderId;


}

