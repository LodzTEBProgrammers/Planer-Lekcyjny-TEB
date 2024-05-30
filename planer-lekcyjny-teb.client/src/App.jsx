import { RouterProvider, createBrowserRouter, createRoutesFromElements, Route } from "react-router-dom"
import './App.css';
import ProtectedRoutes from "@/components/ProtectedRoutes.jsx"
import Home from "@/components/Home.jsx"
import Admin from "@/components/Admin.jsx"
import Login from "@/components/Login.jsx"
import Register from "@/components/Register.jsx"
import Timetable from "@/components/Timetable"

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path='/'>
      <Route element={<ProtectedRoutes />}>
        <Route path='/' element={<Home />} />
        <Route path='/admin' element={<Admin />} />
      </Route>
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='/plan-lekcji' element={<Timetable />} />
      <Route path='*' element={
        <div>
          <header>
            <h1>Not Found</h1>
          </header>
          <p>
            <a href="/">Back to Home</a>
          </p>
        </div>
      } />
    </Route>
  )
);
function App() {
  const isLogged = localStorage.getItem("user");
  const logout = async () => {
    const response = await fetch("/api/SecureWebsite/logout", {
      method: 'GET',
      credentials: 'include',
    });
    
    const data = await response.json();
    if (response.ok) {
      localStorage.removeItem("user");
      alert (data.message);
      document.location = "/login";
    } else {
      console.log("Could not logout", response)
    }
  };
  
  return (
      <section>
      <div className='top-nav'> 
        {
        isLogged ?
        <span className="item-holder">
          <a href="/">Home</a>
          <a href="/admin">Admin</a>
          <a href="/plan-lekcji">Plan Lekcji</a>
          <span onClick={logout}>Log Out</span>
        </span> :
        <span className="item-holder">
          <a href="/login">Login</a>
          <a href="/register">Register</a>
          <a href="/plan-lekcji">Plan Lekcji</a>
        </span>
      }
      </div>
        <RouterProvider router={router}/>
        
      </section>
  );
}

export default App;