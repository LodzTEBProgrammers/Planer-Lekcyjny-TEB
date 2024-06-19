import Admin from "@/components/Admin.jsx"
import Home from "@/components/Home.jsx"
import Login from "@/components/Login.jsx"
import ProtectedRoutes from "@/components/ProtectedRoutes.jsx"
import PublicAnnouncements from "@/components/PublicAnnouncements"
import Register from "@/components/Register.jsx"
import Timetable from "@/components/Timetable"
import { createBrowserRouter, createRoutesFromElements, Route, RouterProvider } from "react-router-dom"
import "./App.css"
import { announcements, clipboard, house, user } from "./assets/navbarIcons"

const router = createBrowserRouter(
	createRoutesFromElements(
		<Route path="/">
			<Route element={<ProtectedRoutes />}>
				<Route path="/" element={<Home />} />
				<Route path="/admin" element={<Admin />} />
			</Route>
			<Route path="/login" element={<Login />} />
			<Route path="/register" element={<Register />} />
			<Route path="/plan-lekcji" element={<Timetable />} />
			<Route path="/ogloszenia" element={<PublicAnnouncements />} />
			<Route
				path="*"
				element={
					<div>
						<header>
							<h1>Not Found</h1>
						</header>
						<p>
							<a href="/">Back to Home</a>
						</p>
					</div>
				}
			/>
		</Route>
	)
)
function App() {
	const isLogged = localStorage.getItem("user")
	const logout = async () => {
		const response = await fetch("/api/SecureWebsite/logout", {
			method: "GET",
			credentials: "include",
		})

		const data = await response.json()
		if (response.ok) {
			localStorage.removeItem("user")
			alert(data.message)
			document.location = "/login"
		} else {
			console.log("Could not logout", response)
		}
	}

	return (
		<>
			<nav>
				{isLogged ? (
					<ul>
						<li>
							<a href="/">
								<img src={house} alt="home" /> Strona główna
							</a>
						</li>
						<li>
							<a href="/admin">
								<img src={user} alt="admin" /> admin
							</a>
						</li>
						<li>
							<a href="/plan-lekcji">
								<img src={clipboard} alt="timetable" /> plan lekcji
							</a>
						</li>
						<li onClick={logout}>log out</li>
					</ul>
				) : (
					<ul>
						<li>
							<img src={user} alt="login" />
							<a href="/login">login</a>
						</li>
						<li>
							<img src={user} alt="register" />
							<a href="/register">register</a>
						</li>
						<li>
							<a href="/plan-lekcji">
								<img src={clipboard} alt="timetable" /> plan lekcji
							</a>
						</li>
						<li>
							<a href="/ogloszenia">
								<img src={announcements} alt="announcements" /> ogloszenia
							</a>
						</li>
					</ul>
				)}
			</nav>
			<RouterProvider router={router} />
		</>
	)
}

export default App
