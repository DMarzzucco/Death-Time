import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneOffset;
import java.util.logging.Logger;

@Component
public class VerifyTimeMiddleware extends OncePerRequestFilter {

    private final Logger logger = Logger.getLogger(VerifyTimeMiddleware.class.getName());

    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
            throws ServletException, IOException {
        try {
            Instant currentTime = Instant.now();
            Instant deathTime = LocalDateTime.parse("0000-00-00T00:00:00")
                    .toInstant(ZoneOffset.UTC);

            if (currentTime.isAfter(deathTime)) {
                logger.info("The deadline has passed, the date has expired.");
                response.setStatus(HttpServletResponse.SC_FORBIDDEN);
                response.getWriter().write("{\"message\": \"the time \"}");
                response.setContentType("application/json");
                return;
            }

            filterChain.doFilter(request, response); 

        } catch (Exception ex) {
            logger.severe("An error occurred: " + ex.getMessage());
            response.setStatus(HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
            response.getWriter().write("{\"message\": \"Server " + ex.getMessage() + "\"}");
            response.setContentType("application/json");
        }
    }
}
