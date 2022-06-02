import { createContext, FC, useContext, useMemo } from 'react';

type AppContextProps = {
  testMessage: string;
};

export const AppContext = createContext<AppContextProps>({
  testMessage: '',
});

export const AppConsumer = AppContext.Consumer;

const AppProvider: FC<any> = ({ children }) => {
  const testMessage = 'cedo-cedo ';

  const state: AppContextProps = useMemo(
    () => ({
      testMessage,
    }),
    [testMessage],
  );
  return <AppContext.Provider value={state}>{children}</AppContext.Provider>;
};

export const useAppStateValue: () => AppContextProps = () =>
  useContext(AppContext);

export default AppProvider;
