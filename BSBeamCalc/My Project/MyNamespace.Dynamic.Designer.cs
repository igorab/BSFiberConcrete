using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CBAnsDes.My
{
    public static partial class MyProject
    {
        public partial class MyForms
        {

            [EditorBrowsable(EditorBrowsableState.Never)]
            public Aboutus m_Aboutus;

            public Aboutus Aboutus
            {
                [DebuggerHidden]
                get
                {
                    m_Aboutus = Create__Instance__(m_Aboutus);
                    return m_Aboutus;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_Aboutus))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_Aboutus);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public addmember m_addmember;

            public addmember addmember
            {
                [DebuggerHidden]
                get
                {
                    m_addmember = Create__Instance__(m_addmember);
                    return m_addmember;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_addmember))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_addmember);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public BeamCreate m_BeamCreate;

            public BeamCreate beamcreate
            {
                //[DebuggerHidden]
                get
                {
                    //igorab
                    m_BeamCreate = Create__Instance__(m_BeamCreate);
                    
                    return m_BeamCreate;
                }
                //[DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_BeamCreate))
                        return;

                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");

                    Dispose__Instance__(ref m_BeamCreate);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public Ends_Editor m_Ends_Editor;

            public Ends_Editor Ends_Editor
            {
                [DebuggerHidden]
                get
                {
                    m_Ends_Editor = Create__Instance__(m_Ends_Editor);
                    return m_Ends_Editor;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_Ends_Editor))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_Ends_Editor);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public GeneralInstruction m_GeneralInstruction;

            public GeneralInstruction GeneralInstruction
            {
                [DebuggerHidden]
                get
                {
                    m_GeneralInstruction = Create__Instance__(m_GeneralInstruction);
                    return m_GeneralInstruction;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_GeneralInstruction))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_GeneralInstruction);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public logo m_logo;

            public logo logo
            {
                [DebuggerHidden]
                get
                {
                    m_logo = Create__Instance__(m_logo);
                    return m_logo;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_logo))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_logo);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public MDIMain m_MDIMain;

            public MDIMain MDIMain
            {
                //[DebuggerHidden]
                get
                {
                    m_MDIMain = Create__Instance__(m_MDIMain);
                    return m_MDIMain;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_MDIMain))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_MDIMain);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public memDetails m_memDetails;

            public memDetails memDetails
            {
                [DebuggerHidden]
                get
                {
                    m_memDetails = Create__Instance__(m_memDetails);
                    return m_memDetails;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_memDetails))
                        return;
                    if (value is not null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_memDetails);
                }
            }

        }


    }
}