import { FC } from 'react';
import { Layout } from 'antd';
import {
  Navigate,
  Route,
  Routes,
  useLocation,
  useParams,
} from 'react-router-dom';
import styled from 'styled-components';

import Groups from 'pages/admin/groups/Groups';
import Runtime from 'pages/admin/runtime/Runtime';
import UserRuntime from 'pages/user/runtime/Runtime';
import EmailVerified from 'pages/email-verify/EmailVerified';
import { Role } from 'util/enums/roles';
import SingleGroup from 'pages/user/groups/SingleGroup';
import Spinner from '../components/spinner/Spinner';
import { useAuthStateValue } from '../context/AuthContext';
import PageProvider from '../context/PageContext';

import PContent from '../layout/content/PContent';
import PHeader from '../layout/header/PHeader';
import PSider from '../layout/sider/PSider';

import SpinnerPage from '../pages/SpinnerPage';

// routes
import AdminRoutes from './AdminRoutes';
import UserRoutes from './UserRoutes';
import Error from '../pages/error/ErrorPage';
import Login from '../pages/login/Login';
import EmailVerify from '../pages/email-verify/EmailVerify';
import EmailConfirmed from '../pages/email-verify/EmailConfirmed';
// admin routes
import Home from '../pages/admin/home/Home';
import Admins from '../pages/admin/admins/Admins';
import Files from '../pages/admin/files/Files';
import Users from '../pages/admin/users/Users';
// user routes
import UserHome from '../pages/user/home/Home';
import UserFiles from '../pages/user/files/Files';
import Profile from '../pages/user/profile/Profile';
import UserGroups from '../pages/user/groups/Groups';

const PRouter: FC = () => {
  const { isAuthenticated, isAdmin, loggedUser, isLoading } =
    useAuthStateValue();
  const location = useLocation();

  // if (!isLoading) {
  if (isAuthenticated) {
    return (
      <StyledLayout>
        <PHeader />
        <Layout>
          <PageProvider>
            <PSider />
            <PContent>
              <Routes>
                <Route
                  element={
                    <AdminRoutes
                      isAdmin={loggedUser === Role.ADMIN}
                      location={location}
                    />
                  }
                >
                  <Route path="/" element={<Home />} />
                  <Route path="/files" element={<Files />} />
                  <Route path="/admins" element={<Admins />} />
                  <Route path="/users" element={<Users />} />
                  <Route path="/groups" element={<Groups />} />
                  <Route path="/runtime" element={<Runtime />} />
                </Route>
                <Route
                  element={<UserRoutes isUser={loggedUser === Role.USER} />}
                >
                  <Route path="/user-home" element={<UserHome />} />
                  <Route path="/user-files" element={<UserFiles />} />
                  <Route path="/profile" element={<Profile />} />
                  <Route path="/user-groups" element={<UserGroups />} />
                  <Route path="/user-groups/:id" element={<SingleGroup />} />
                  <Route path="/user-runtime" element={<UserRuntime />} />
                </Route>
                {/* <Route path="/confirm" element={<EmailConfirmed />} /> */}
                <Route
                  path="/notallowed"
                  element={<Error message="Cant go here" />}
                />
                <Route
                  path="*"
                  element={<Error message="Router error 404" />}
                />
                {/* if user is logged in, user cant get /login route */}
                <Route path="/login" element={<Navigate to="/" />} />
              </Routes>
            </PContent>
          </PageProvider>
        </Layout>
      </StyledLayout>
    );
  }

  return (
    <Routes>
      <Route path="/verified" element={<EmailVerified />} />
      <Route path="/loading" element={<SpinnerPage />} />
      <Route path="/verification" element={<EmailVerify />} />
      <Route path="/confirm" element={<EmailConfirmed />} />
      <Route path="/login" element={<Login />} />
      {/* <Route path="*" element={<Login />} /> */}
      <Route
        path="*"
        element={<Navigate to={isLoading ? '/loading' : '/'} />}
      />
    </Routes>
  );
};
// return (
//   <StyledPage>
//     <SpinnerWrapper>
//       <Spinner color="#000" size={42} speed={2} />
//     </SpinnerWrapper>
//   </StyledPage>
// );
// };

export default PRouter;

const SpinnerWrapper = styled.div`
  height: 200px;
  width: 200px;
  display: flex;
  justify-content: center;
  align-items: center;
`;

const StyledPage = styled.div`
  height: 100vh;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f0f2f5;
`;
const StyledLayout = styled(Layout)`
  height: 100vh;
`;
