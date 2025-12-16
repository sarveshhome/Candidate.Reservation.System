function Layout({ children }) {
  return (
    <div className="container-fluid">
      <nav className="navbar navbar-dark bg-primary mb-4">
        <div className="container">
          <span className="navbar-brand">Candidate Selection System</span>
        </div>
      </nav>
      {children}
    </div>
  );
}

export default Layout;