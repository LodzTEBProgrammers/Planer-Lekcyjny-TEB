import { useEffect } from "react"
import "../../components/RegisterAndLoginCSS/style.css"

function Register() {
	document.title = "Register"

	// dont ask an already registered user to register over and over again
	useEffect(() => {
		const user = localStorage.getItem("user")
		if (user) {
			document.location = "/"
		}
	}, [])

	return (
		<section className="register-page-wrapper page">
			<div className="logowanie">
				<header>
					<h1>Strona Rejestracji</h1>
				</header>

				<form action="#" className="login" onSubmit={registerHandler}>
					<div className="input-div">
						<input type="text" name="Name" id="name" required placeholder="Imie" />
					</div>
					<div className="input-div">
						<input type="email" name="Email" id="email" required placeholder="E-mail" />
					</div>
					<div className="input-div">
						<input type="password" name="PasswordHash" id="password" required placeholder="Hasło" />
					</div>

					<div className="rem-pass">
						<label htmlFor="checkbox">
							Zapamiętać <span>Hasło?</span>
						</label>
						<input type="checkbox" name="Remember" id="remember" />
					</div>
					<input type="submit" value="Register" className="login-btn" />
					<div className="register">
						<span>masz konto?</span>
						<a href="/login">Zaloguj sie</a>
					</div>
					<p className="message"></p>
				</form>
			</div>
		</section>
	)
	async function registerHandler(e) {
		e.preventDefault()
		const form_ = e.target,
			submitter = document.querySelector("input.login")

		const formData = new FormData(form_, submitter),
			dataToSend = {}

		for (const [key, value] of formData) {
			dataToSend[key] = value
		}

		// create username
		const newUserName = dataToSend.Name.trim().split(" ")
		dataToSend.UserName = newUserName.join("")

		const response = await fetch("api/securewebsite/register", {
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
			document.location = "/login"
		}

		const messageEl = document.querySelector(".message")
		if (data.message) {
			messageEl.innerHTML = data.message
		} else {
			let errorMessages = "<div>Attention please:</div><div class='normal'>"
			data.errors.forEach((error) => {
				errorMessages += error.description + " "
			})

			errorMessages += "</div>"
			messageEl.innerHTML = errorMessages
		}

		console.log("login error: ", data)
	}
}

export default Register
