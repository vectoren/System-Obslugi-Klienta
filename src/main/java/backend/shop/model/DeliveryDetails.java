package backend.shop.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
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
    private Integer id;
    private String region; //Woj
    private String city;//miasto
    private String street;//ulica
    private String townCode;//kod pocztowy
    private String homeNumber;//nr domu
    private Date deliveredDate;
    private double deliveryCost;
}

