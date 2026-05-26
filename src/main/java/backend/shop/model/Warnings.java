package backend.shop.model;

import jakarta.persistence.*;
import lombok.*;

import java.util.Date;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Entity
public class Warnings{
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer warningId;
    private String description;
    private Date recivedDate;
    private boolean isReady;
    private String type; //warning, error itp

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "orderId", nullable = false)
    private Orders orderId;

}
