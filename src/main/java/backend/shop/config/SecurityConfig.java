package backend.shop.config;

import backend.shop.service.UserProfilerService;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
public class SecurityConfig {

    @Autowired
    private UserProfilerService userProfilerService;

    @Bean
    public SecurityFilterChain userDetails(HttpSecurity http){
        return http.csrf(AbstractHttpConfigurer::disable)
                .authorizeHttpRequests(
                        r ->
                                r.requestMatchers("/api/register", "/api/login", "/login").permitAll()
                                .anyRequest().authenticated())
                .formLogin(f ->
                        f.loginProcessingUrl("/api/login")
                                .usernameParameter("username")
                                .passwordParameter("password")
                                .successHandler(((request, response, authentication) -> {
                                    response.setStatus(HttpServletResponse.SC_OK);
                                }))
                                .failureHandler(((request, response, exception) -> {
                                    response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
                                })
                                )
                )
                .exceptionHandling(e -> e
                .authenticationEntryPoint((request, response, authException) -> {
                    response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
                    response.getWriter().write("Unauthorized - Brak dostepu.");
                })
                )
                .logout(l ->
                        l.logoutUrl("/api/logout")
                                .logoutSuccessHandler(((request, response, authentication) -> {
                                    response.setStatus(HttpServletResponse.SC_OK);
                                }))
                )
                .build();

    }

    @Bean
    public AuthenticationProvider authenticationProvider(){
        var provider = new DaoAuthenticationProvider(userProfilerService);
        provider.setPasswordEncoder(new BCryptPasswordEncoder());
        return provider;
    }

}
