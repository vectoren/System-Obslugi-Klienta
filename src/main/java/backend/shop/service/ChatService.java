package backend.shop.service;

import backend.shop.model.ChatMessage;
import backend.shop.repo.ChatRepo;
import org.springframework.stereotype.Service;

@Service
public class ChatService {
    private final ChatRepo repo;

    public ChatService(ChatRepo repo) {
        this.repo = repo;
    }

    public boolean saveMessage(ChatMessage message) {
        try{
            this.repo.save(message);
            return true;
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return false;
        }
    }
}
