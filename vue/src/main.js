import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import Keycloak from "keycloak-js";

let initOptions = {
  url: "http://localhost:8080",
  realm: "dotnet-vue",
  clientId: "vue",
  onLoad: "login-required",
};

let keycloak = new Keycloak(initOptions);

keycloak
  .init({ onLoad: initOptions.onLoad })
  .then((auth) => {
    if (!auth) {
      window.location.reload();
    } else {
      console.log("Authenticated");
    }
    createApp(App).mount("#app");

    setInterval(() => {
      keycloak
        .updateToken(70)
        .success((refreshed) => {
          if (refreshed) {
            console.log("Token refreshed" + refreshed);
          } else {
            console.warn(
              "Token not refreshed, valid for " +
                Math.round(
                  keycloak.tokenParsed.exp +
                    keycloak.timeSkew -
                    new Date().getTime() / 1000
                ) +
                " seconds"
            );
          }
        })
        .error(() => {
          console.error("Failed to refresh token");
        });
    }, 60000);
  })
  .catch((error) => {
    console.log(error);
    console.error("Authenticated Failed");
  });


export default keycloak;
