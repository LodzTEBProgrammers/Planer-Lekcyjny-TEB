import { useEffect } from "react"
import "../../components/RegisterAndLoginCSS/style.css"
function Login() {
	document.title = "Login"

	// dont ask an already logged in user to login over and over again
	useEffect(() => {
		const user = localStorage.getItem("user")
		if (user) {
			document.location = "/"
		}
	}, [])

	return (
		<section className="login-page-wrapper page">
			<div className="logowanie">
				<header>
					<h1>Strona Logowania</h1>
				</header>

				<form action="#" className="login" onSubmit={loginHandler}>
					<div className="input-div">
						<input type="email" name="Email" id="email" required placeholder="Email" />
					</div>
					<div className="input-div">
						<input type="password" name="Password" id="password" required placeholder="Hasło" />
					</div>

					<div className="rem-pass">
						<label htmlFor="checkbox">
							Zapamiętać <span>Hasło?</span>
						</label>
						<input type="checkbox" name="Remember" id="remember" />
					</div>
					<input type="submit" value="Login" className="login-btn" />
					<div className="register">
						<span>Nie masz konta?</span>
						<a href="/register">Zarejestruj sie</a>
					</div>
					<p className="message"></p>
				</form>
			</div>
		</section>
	)
	async function loginHandler(e) {
		e.preventDefault()
		const form_ = e.target,
			submitter = document.querySelector("input.login")

		const formData = new FormData(form_, submitter),
			dataToSend = {}

		for (const [key, value] of formData) {
			dataToSend[key] = value
		}

		if (dataToSend.Remember === "on") {
			dataToSend.Remember = true
		}

		console.log("login data before send: ", dataToSend)
		const response = await fetch("api/securewebsite/login", {
			method: "POST",
			credentials: "include",
			body: JSON.stringify(dataToSend),
			headers: {
				"content-type": "Application/json",
				Accept: "application/json",
			},
		})

		const data = await response.json()

		if (response.ok) {
			localStorage.setItem("user", dataToSend.Email)
			document.location = "/"
		}

		const messageEl = document.querySelector(".message")
		if (data.message) {
			messageEl.innerHTML = data.message
		} else {
			messageEl.innerHTML = "Something went wrong, please try again"
		}

		console.log("login error: ", data)
	}
}

export default Login
