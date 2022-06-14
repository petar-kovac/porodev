import { createContext, FC, useContext, useMemo, useState } from 'react';

type AppContextProps = {
  testMessage: string;
  isLoading: boolean;
};

export const AppContext = createContext<AppContextProps>({
  testMessage: '',
  isLoading: true,
});

export const AppConsumer = AppContext.Consumer;

const AppProvider: FC<any> = ({ children }) => {
  const testMessage = 'cedo-cedo ';
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const state: AppContextProps = useMemo(
    () => ({
      testMessage,
      isLoading,
    }),
    [testMessage],
  );
  return <AppContext.Provider value={state}>{children}</AppContext.Provider>;
};

export const useAppStateValue: () => AppContextProps = () =>
  useContext(AppContext);

export default AppProvider;
